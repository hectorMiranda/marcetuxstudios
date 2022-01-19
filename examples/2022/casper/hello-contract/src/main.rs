// Minimal Casper smart contract: stores a u64 value under a named key.
// Compiles to wasm32-unknown-unknown; deploy with casper-client put-deploy.
#![no_std]
#![no_main]

extern crate alloc;
use alloc::string::String;

use casper_contract::contract_api::{runtime, storage};
use casper_types::URef;

#[no_mangle]
pub extern "C" fn store_value() {
    // Read the "value" argument passed in the deploy session args.
    let value: u64 = runtime::get_named_arg("value");

    // Allocate a new URef in global state and write the value.
    let value_ref: URef = storage::new_uref(value);

    // Put it under a named key so callers can find it later.
    runtime::put_key("stored_value", value_ref.into());
}

#[panic_handler]
fn panic(_: &core::panic::PanicInfo) -> ! {
    loop {}
}
