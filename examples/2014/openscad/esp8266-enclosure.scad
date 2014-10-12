// ESP8266-01 enclosure — a snap-fit box sized for the 14.3mm × 24.8mm module.
// Adjust the variables at the top to fit your module dimensions.

module_w  = 14.8;   // module width  + 0.5mm clearance
module_d  = 25.3;   // module depth  + 0.5mm clearance
module_h  = 8.0;    // module height (with pins)
wall      = 1.6;    // wall thickness
lid_h     = 2.0;    // lid overlap height

// Outer box
difference() {
    cube([module_w + wall*2, module_d + wall*2, module_h + wall + lid_h]);
    // Inner cavity
    translate([wall, wall, wall])
        cube([module_w, module_d, module_h + lid_h + 1]);
    // Antenna cutout (top)
    translate([wall + 2, module_d + wall - 1, wall + 2])
        cube([module_w - 4, 2 + wall + 1, module_h - 4]);
    // Serial header access (side)
    translate([wall - 0.5, wall + 2, wall + 1])
        cube([1 + wall, module_d - 4, module_h - 2]);
}
