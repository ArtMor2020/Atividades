<?php
function displayFilePreview($filePath, $fileType) {
    if ($fileType === 'image') {
        echo "<img src='" . htmlspecialchars($filePath) . "' alt='File Preview' style='max-width: 100%; max-height: 200px; object-fit: cover;'>";
    
    } elseif ($fileType === 'text') {
        echo "<div class='text-preview'>";
        echo "<pre>" . htmlspecialchars(file_get_contents($filePath)) . "</pre>";
        echo "</div>";

    } elseif ($fileType === 'pdf') {
        echo "<embed src='" . htmlspecialchars($filePath) . "' type='application/pdf' style='width: 100%; height: 200px;'>";
    
    } elseif ($fileType === 'video') {
        echo "<video controls style='width: 100%; max-height: 200px; object-fit: cover;'>";
        echo "<source src='" . htmlspecialchars($filePath) . "' type='video/mp4'>";
        echo "</video>";

    } elseif ($fileType === 'audio') {
        echo "<audio controls style='width: 100%;'>";
        echo "<source src='" . htmlspecialchars($filePath) . "' type='audio/mpeg'>";
        echo "</audio>";

    } else {
        echo "Preview not available for this file type.";
    }
}
?>
