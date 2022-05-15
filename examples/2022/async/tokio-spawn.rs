// Tokio async example: spawn two concurrent tasks, join their results.
// Requires: tokio = { version = "1", features = ["full"] } in Cargo.toml
use tokio::task;

async fn fetch_balance(account: &str) -> u64 {
    // Simulate async work (network call to a Casper node, etc.)
    tokio::time::sleep(std::time::Duration::from_millis(50)).await;
    println!("fetched balance for {account}");
    1_000_000u64
}

#[tokio::main]
async fn main() {
    // Spawn two futures concurrently; both run on the Tokio thread pool.
    let t1 = task::spawn(async { fetch_balance("account-hash-aaa").await });
    let t2 = task::spawn(async { fetch_balance("account-hash-bbb").await });

    // Wait for both to finish and collect results.
    let (b1, b2) = tokio::join!(t1, t2);
    println!("balances: {} + {}", b1.unwrap(), b2.unwrap());
}
