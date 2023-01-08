use thiserror::Error;

#[derive(Debug, Error)]
pub enum ContractError {
    #[error("rpc call failed: {0}")]
    Rpc(#[from] casper_client::Error),
    #[error("deploy rejected: {reason}")]
    DeployRejected { reason: String },
    #[error("timeout after {secs}s")]
    Timeout { secs: u64 },
    #[error("invalid response: {0}")]
    Parse(#[from] serde_json::Error),
}

// In your calling code, ? propagates automatically.
pub async fn send_deploy(deploy: Deploy) -> Result<DeployHash, ContractError> {
    let hash = client::put_deploy(deploy).await?;   // Rpc variant if it fails
    Ok(hash)
}
