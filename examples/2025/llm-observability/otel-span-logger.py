# Wrap an LLM call with an OpenTelemetry span that captures prompt + completion.
# Keep content gated behind a feature flag so you can disable logging in prod.
from opentelemetry import trace
from opentelemetry.trace import StatusCode

tracer = trace.get_tracer("llm.client")

def chat(client, model: str, prompt: str, *, log_content: bool = False) -> str:
    with tracer.start_as_current_span("llm.chat") as span:
        span.set_attribute("llm.model", model)
        span.set_attribute("llm.prompt.tokens", len(prompt.split()))
        if log_content:
            span.set_attribute("llm.prompt", prompt[:4096])
        try:
            resp = client.chat(model=model, messages=[{"role": "user", "content": prompt}])
            text = resp.choices[0].message.content
            span.set_attribute("llm.completion.tokens", len(text.split()))
            if log_content:
                span.set_attribute("llm.completion", text[:4096])
            span.set_status(StatusCode.OK)
            return text
        except Exception as exc:
            span.record_exception(exc)
            span.set_status(StatusCode.ERROR, str(exc))
            raise
