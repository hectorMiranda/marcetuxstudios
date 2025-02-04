# Minimal MCP server (Python SDK) wrapping a REST ticket API.
# Run with: python ticket-server.py --token $API_TOKEN
import argparse, httpx
from mcp.server import Server
from mcp.server.stdio import stdio_server
from mcp.types import Tool, TextContent

parser = argparse.ArgumentParser()
parser.add_argument("--token", required=True)
args = parser.parse_args()

BASE = "https://tracker.internal/api/v1"
HEADERS = {"Authorization": f"Bearer {args.token}", "Accept": "application/json"}

server = Server("ticket-server")

@server.list_tools()
async def list_tools():
    return [
        Tool(name="search_tickets", description="Search tickets by keyword",
             inputSchema={"type":"object","properties":{"q":{"type":"string"}},"required":["q"]}),
        Tool(name="create_ticket", description="Create a new ticket",
             inputSchema={"type":"object","properties":{"title":{"type":"string"},"body":{"type":"string"}},"required":["title","body"]}),
    ]

@server.call_tool()
async def call_tool(name: str, arguments: dict):
    async with httpx.AsyncClient(headers=HEADERS) as client:
        if name == "search_tickets":
            r = await client.get(f"{BASE}/tickets", params={"q": arguments["q"]})
            r.raise_for_status()
            return [TextContent(type="text", text=r.text)]
        if name == "create_ticket":
            r = await client.post(f"{BASE}/tickets", json=arguments)
            r.raise_for_status()
            return [TextContent(type="text", text=r.text)]
        raise ValueError(f"Unknown tool: {name}")

if __name__ == "__main__":
    import asyncio
    asyncio.run(stdio_server(server))
