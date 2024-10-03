# Compresses old conversation turns into a summary once the turn count exceeds a threshold.
# Requires an LLM client with a .complete(messages) -> str interface.
from dataclasses import dataclass, field
from typing import Optional

SUMMARY_THRESHOLD = 20   # compress when turns exceed this
KEEP_RECENT = 6          # always keep the N most-recent turns verbatim

@dataclass
class ConversationMemory:
    turns: list[dict] = field(default_factory=list)
    summary: Optional[str] = None

    def add(self, role: str, content: str) -> None:
        self.turns.append({"role": role, "content": content})

    def context(self, client) -> list[dict]:
        """Return context window messages, compressing old turns if needed."""
        if len(self.turns) <= SUMMARY_THRESHOLD:
            return self._with_summary(self.turns)
        # Compress old turns; keep recent ones verbatim
        old, recent = self.turns[:-KEEP_RECENT], self.turns[-KEEP_RECENT:]
        self.summary = client.complete(
            [{"role": "user", "content":
              f"Summarize this conversation concisely:\n{_fmt(old)}\n"
              f"Existing summary to extend: {self.summary or 'none'}"}]
        )
        self.turns = recent
        return self._with_summary(recent)

    def _with_summary(self, turns: list[dict]) -> list[dict]:
        if not self.summary:
            return turns
        return [{"role": "system", "content": f"Prior context: {self.summary}"}] + turns

def _fmt(turns: list[dict]) -> str:
    return "\n".join(f"{t['role']}: {t['content']}" for t in turns)
