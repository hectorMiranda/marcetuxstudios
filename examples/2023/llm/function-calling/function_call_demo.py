import json
import openai

# Declare the tool the model can call.
FUNCTIONS = [
    {
        "name": "cite_and_answer",
        "description": "Return an answer grounded in the provided context chunks.",
        "parameters": {
            "type": "object",
            "properties": {
                "answer": {
                    "type": "string",
                    "description": "The answer to the user's question.",
                },
                "source_ids": {
                    "type": "array",
                    "items": {"type": "string"},
                    "description": "IDs of the context chunks used to produce the answer.",
                },
            },
            "required": ["answer", "source_ids"],
        },
    }
]

def ask_with_context(question: str, context_chunks: list[dict]) -> dict:
    context_text = "\n\n".join(
        f"[{c['id']}] {c['text']}" for c in context_chunks
    )
    messages = [
        {"role": "system", "content": "Answer only from the provided context."},
        {"role": "user", "content": f"Context:\n{context_text}\n\nQuestion: {question}"},
    ]
    response = openai.ChatCompletion.create(
        model="gpt-3.5-turbo-0613",
        messages=messages,
        functions=FUNCTIONS,
        function_call={"name": "cite_and_answer"},
    )
    args = response.choices[0].message["function_call"]["arguments"]
    return json.loads(args)
