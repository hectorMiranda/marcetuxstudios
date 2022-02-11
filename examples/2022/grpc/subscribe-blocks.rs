// Subscribe to finalized block events from a Casper node via gRPC server-streaming.
// Requires tonic and a compiled proto from the casper-node repo.
use tonic::transport::Channel;
use tonic::Request;

// Assume generated stubs are in `casper::node::v1`:
// use casper::node::v1::casper_client::CasperClient;
// use casper::node::v1::SubscribeRequest;

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    let channel = Channel::from_static("http://[::1]:7777").connect().await?;
    // let mut client = CasperClient::new(channel);

    // let request = Request::new(SubscribeRequest {
    //     events: vec![EventType::FinalizedBlock as i32],
    // });

    // let mut stream = client.subscribe(request).await?.into_inner();

    // while let Some(event) = stream.message().await? {
    //     println!("block: {:?}", event);
    // }

    // Skeleton; fill in once proto stubs are generated.
    println!("gRPC channel ready: {:?}", channel);
    Ok(())
}
