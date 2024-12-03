#!/usr/bin/env python3
"""
Minimal MCP server (stdio transport) that serves a directory of .md files as resources.
Handles: initialize, resources/list, resources/read — enough for a basic host.
Run: python minimal_mcp_server.py --notes-dir ./notes
"""
import json, sys, pathlib, argparse

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--notes-dir", default="./notes")
    args = parser.parse_args()
    notes_dir = pathlib.Path(args.notes_dir)

    for raw_line in sys.stdin:
        try:
            req = json.loads(raw_line)
        except json.JSONDecodeError:
            continue
        method = req.get("method")
        req_id = req.get("id")

        if method == "initialize":
            respond(req_id, {
                "protocolVersion": "0.1.0",
                "capabilities": {"resources": {}},
                "serverInfo": {"name": "notes-server", "version": "0.1.0"},
            })
        elif method == "resources/list":
            resources = [
                {"uri": f"notes://{p.stem}", "name": p.stem, "mimeType": "text/markdown"}
                for p in sorted(notes_dir.glob("*.md"))
            ]
            respond(req_id, {"resources": resources})
        elif method == "resources/read":
            uri = req.get("params", {}).get("uri", "")
            stem = uri.removeprefix("notes://")
            target = notes_dir / f"{stem}.md"
            if target.exists():
                respond(req_id, {"contents": [{"uri": uri, "mimeType": "text/markdown",
                                                "text": target.read_text()}]})
            else:
                error(req_id, -32602, f"Resource not found: {uri}")
        else:
            error(req_id, -32601, f"Unknown method: {method}")

def respond(req_id, result):
    print(json.dumps({"jsonrpc": "2.0", "id": req_id, "result": result}), flush=True)

def error(req_id, code, message):
    print(json.dumps({"jsonrpc": "2.0", "id": req_id,
                       "error": {"code": code, "message": message}}), flush=True)

if __name__ == "__main__":
    main()
