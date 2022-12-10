# Prompting patterns when using ChatGPT for technical verification

## The "source it" pattern
When you get a confident answer, follow with:
"What's your source for that? Is this from the official docs, a specific RFC,
or are you reasoning from general knowledge?"
If it can't cite a source, treat the answer as a hypothesis to verify.

## The "adversarial" pattern
After getting an answer, ask:
"What would make this answer wrong? What are the edge cases or counterexamples
I should test?"
Forces the model to surface its uncertainty instead of projecting confidence.

## The "unknown API" pattern
When asking about a library you know is obscure:
"Before you answer, tell me if you're confident you have accurate documentation
for [library name] or if you're extrapolating from similar libraries."
This primes more honest uncertainty expressions.

## The "rubber duck" pattern
Describe the problem without asking for a solution:
"I'm having a borrow checker error in this Rust function — [paste code]. Walk
me through what the compiler is seeing."
The explanation is often more useful than the solution.
