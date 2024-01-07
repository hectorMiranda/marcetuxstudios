# Splits text at paragraph boundaries and adds a token overlap window
# so context doesn't vanish at chunk seams. Chunk size is approximate.
from typing import Generator

def chunk_paragraphs(text: str, max_tokens: int = 400, overlap: int = 50) -> Generator[str, None, None]:
    """Yield overlapping paragraph-boundary chunks."""
    paras = [p.strip() for p in text.split("\n\n") if p.strip()]
    current: list[str] = []
    current_len = 0

    for para in paras:
        para_len = len(para.split())
        if current_len + para_len > max_tokens and current:
            yield "\n\n".join(current)
            # keep last <overlap> tokens worth of paragraphs for context
            while current and current_len > overlap:
                removed = current.pop(0)
                current_len -= len(removed.split())
        current.append(para)
        current_len += para_len

    if current:
        yield "\n\n".join(current)
