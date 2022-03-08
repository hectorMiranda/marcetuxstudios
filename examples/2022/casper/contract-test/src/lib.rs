// Skeleton showing how to use casper-engine-test-support to call a contract
// entry point and assert on the resulting named key.
#[cfg(test)]
mod tests {
    use casper_engine_test_support::{
        ExecuteRequestBuilder, InMemoryWasmTestBuilder, DEFAULT_ACCOUNT_ADDR,
        DEFAULT_RUN_GENESIS_REQUEST,
    };
    use casper_types::RuntimeArgs;

    const CONTRACT_WASM: &str = "hello_contract.wasm";

    #[test]
    fn test_store_value_entry() {
        let mut builder = InMemoryWasmTestBuilder::default();
        builder.run_genesis(&DEFAULT_RUN_GENESIS_REQUEST).commit();

        let exec_request = ExecuteRequestBuilder::standard(
            *DEFAULT_ACCOUNT_ADDR,
            CONTRACT_WASM,
            RuntimeArgs::new(),
        )
        .build();

        builder.exec(exec_request).expect_success().commit();

        // After execution, inspect the named key stored by the contract:
        // let account = builder.get_account(*DEFAULT_ACCOUNT_ADDR).unwrap();
        // let key = account.named_keys().get("stored_value").cloned().unwrap();
        // let value: u64 = builder.query(None, key, &[]).unwrap().as_cl_value()...;
        // assert_eq!(value, 42);
    }
}
