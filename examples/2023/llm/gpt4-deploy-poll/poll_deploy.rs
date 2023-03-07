use std::time::{Duration, Instant};
use casper_client::{get_deploy, rpcs::results::GetDeployResult};

const POLL_INTERVAL: Duration = Duration::from_secs(3);
const TIMEOUT: Duration = Duration::from_secs(120);

pub async fn wait_for_finality(
    node_address: &str,
    deploy_hash: &str,
) -> Result<GetDeployResult, String> {
    let start = Instant::now();

    loop {
        if start.elapsed() > TIMEOUT {
            return Err(format!("timed out waiting for deploy {deploy_hash}"));
        }

        let result = get_deploy(node_address, deploy_hash)
            .await
            .map_err(|e| e.to_string())?;

        // Finalized AND execution_result present AND not an error.
        if result.is_stored_value() {
            if let Some(exec) = result.execution_results.first() {
                match &exec.result {
                    casper_types::ExecutionResult::Success { .. } => return Ok(result),
                    casper_types::ExecutionResult::Failure { error_message, .. } => {
                        return Err(format!("deploy reverted: {error_message}"));
                    }
                }
            }
        }

        tokio::time::sleep(POLL_INTERVAL).await;
    }
}
