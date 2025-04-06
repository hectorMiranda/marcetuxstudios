# Split a document into chunks at sentence boundaries, targeting a token budget.
# Sentence-boundary splits preserve context that fixed-size splits destroy.
import re
from typing import Generator

SENTENCE_END = re.compile(r'(?<=[.!?])\s+')

def sentences(text: str) -> list[str]:
    return [s.strip() for s in SENTENCE_END.split(text) if s.strip()]

def semantic_chunks(
    text: str,
    max_tokens: int = 300,
    overlap_sentences: int = 1,
    chars_per_token: float = 4.0,
) -> Generator[str, None, None]:
    """Yield overlapping chunks that respect sentence boundaries."""
    sents = sentences(text)
    max_chars = int(max_tokens * chars_per_token)
    buf: list[str] = []
    buf_len = 0

    for i, sent in enumerate(sents):
        sent_len = len(sent)
        if buf and buf_len + sent_len + 1 > max_chars:
            yield " ".join(buf)
            # Keep last N sentences as overlap
            buf = buf[-overlap_sentences:] if overlap_sentences else []
            buf_len = sum(len(s) for s in buf) + len(buf)
        buf.append(sent)
        buf_len += sent_len + 1

    if buf:
        yield " ".join(buf)
