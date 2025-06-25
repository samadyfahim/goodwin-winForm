# Create Sample Machine Images Script
Write-Host "Creating sample machine images..." -ForegroundColor Green

# Ensure the directory exists
$imageDir = "C:\GoodwinImages\Machines"
if (!(Test-Path $imageDir)) {
    New-Item -ItemType Directory -Path $imageDir -Force
    Write-Host "Created directory: $imageDir" -ForegroundColor Green
}

# Create simple text files as placeholders for now
$machines = @(
    @{Name="Production Line 1"; Serial="PL001-2024"; File="production_line_1.jpg"},
    @{Name="Packaging Machine Alpha"; Serial="PK001-2024"; File="packaging_machine_alpha.jpg"},
    @{Name="CNC Milling Machine Bravo"; Serial="CNC001-2024"; File="cnc_milling_bravo.jpg"},
    @{Name="Quality Control Scanner"; Serial="QC001-2024"; File="quality_control_scanner.jpg"},
    @{Name="Assembly Robot Delta"; Serial="ROB001-2024"; File="assembly_robot_delta.jpg"}
)

foreach ($machine in $machines) {
    $filePath = Join-Path $imageDir $machine.File
    $content = @"
Sample Image for $($machine.Name)
Serial Number: $($machine.Serial)
Generated: $(Get-Date)
"@
    
    # Create a simple text file as placeholder
    $content | Out-File -FilePath $filePath -Encoding UTF8
    Write-Host "Created: $filePath" -ForegroundColor Green
}

Write-Host ""
Write-Host "Summary:" -ForegroundColor Cyan
$imageCount = (Get-ChildItem $imageDir -Filter "*.jpg").Count
Write-Host "Total images created: $imageCount" -ForegroundColor White
Write-Host "Image directory: $imageDir" -ForegroundColor White

Write-Host ""
Write-Host "Sample images created successfully!" -ForegroundColor Green
Write-Host "You can now run your application to see the machine images." -ForegroundColor White 