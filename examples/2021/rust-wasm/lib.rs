// Monte Carlo option pricing kernel compiled to WebAssembly via wasm-pack.
// Build: wasm-pack build --target web
// Import the generated npm package in JS; pass paths as Float64Array.
use wasm_bindgen::prelude::*;

#[wasm_bindgen]
pub fn price_options(spots: &[f64], strike: f64, rate: f64, vol: f64, dt: f64) -> Vec<f64> {
    spots
        .iter()
        .map(|&s| {
            // Simple Black-Scholes terminal price (single step, illustrative)
            let drift = (rate - 0.5 * vol * vol) * dt;
            let diffusion = vol * dt.sqrt();
            // pseudo-random using spot as seed — real impl uses a proper PRNG
            let z: f64 = (s * 13.37).sin();
            let terminal = s * (drift + diffusion * z).exp();
            let payoff = (terminal - strike).max(0.0);
            payoff * (-rate * dt).exp()
        })
        .collect()
}
