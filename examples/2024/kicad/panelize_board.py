# Run in the KiCad PCB scripting console (Tools → Scripting Console).
# Duplicates the loaded board into a 2×2 panel with 2 mm spacing.
# Adjust COLS, ROWS, and SPACING_MM to taste.
import pcbnew

COLS = 2
ROWS = 2
SPACING_MM = 2.0

board = pcbnew.GetBoard()
bounding_box = board.GetBoardEdgesBoundingBox()
w = pcbnew.ToMM(bounding_box.GetWidth())
h = pcbnew.ToMM(bounding_box.GetHeight())

origin_x = pcbnew.ToMM(bounding_box.GetX())
origin_y = pcbnew.ToMM(bounding_box.GetY())

for col in range(COLS):
    for row in range(ROWS):
        if col == 0 and row == 0:
            continue  # keep original in place
        dx = pcbnew.FromMM(col * (w + SPACING_MM))
        dy = pcbnew.FromMM(row * (h + SPACING_MM))
        for item in board.GetFootprints():
            new_item = item.Duplicate()
            pos = item.GetPosition()
            new_item.SetPosition(pcbnew.VECTOR2I(pos.x + dx, pos.y + dy))
            board.Add(new_item)
        # Duplicate edge cuts for this cell
        for drawing in board.GetDrawings():
            new_d = drawing.Duplicate()
            pos = drawing.GetStart()
            new_d.Move(pcbnew.VECTOR2I(dx, dy))
            board.Add(new_d)

pcbnew.Refresh()
print(f"Panel: {COLS}x{ROWS} copies placed.")
