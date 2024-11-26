<?php

require_once "../Repositories/FileTagRepository.php";

$tagId = isset($_GET['tagId']) ? (int) $_GET['tagId'] : 0;

if ($tagId <= 0) {
    echo "Invalid tag ID.";
    exit;
}

$fileTagRepo = new FileTagRepository();

$files = $fileTagRepo->getFilesOnTag($tagId);

$tagRepo = new TagRepository();
$tag = $tagRepo->getTag($tagId);

if (!$tag) {
    echo "Tag not found.";
    exit;
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Files Associated with Tag: <?php echo htmlspecialchars($tag->getName()); ?></title>
    <link rel="stylesheet" href="styles.css">
    <style>
        .file-list {
            margin-top: 20px;
        }
        .file-item {
            background-color: #f4f4f4;
            padding: 10px;
            margin: 5px 0;
            border-radius: 5px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            cursor: pointer;
        }
        .file-item:hover {
            background-color: #e0e0e0;
        }
        .file-name {
            font-weight: bold;
            font-size: 16px;
        }
        .file-type {
            font-size: 14px;
            color: #888;
        }
    </style>
</head>
<body>

    <?php include 'topbar.php'; ?>

    <div class="content">
        <h1>Files Associated with Tag: <?php echo htmlspecialchars($tag->getName()); ?></h1>

        <?php if ($files): ?>
            <div class="file-list">
                <?php foreach ($files as $file): ?>
                    <div class="file-item" onclick="openRightSidebar(<?php echo $file->getId(); ?>, '<?php echo addslashes($file->getName()); ?>', '<?php echo addslashes($file->getPath()); ?>', '<?php echo addslashes($file->getType()); ?>')">
                        <span class="file-name"><?php echo htmlspecialchars($file->getName()); ?></span>
                        <span class="file-type"><?php echo htmlspecialchars($file->getType()); ?></span>
                    </div>
                <?php endforeach; ?>
            </div>
        <?php else: ?>
            <p>No files found for this tag.</p>
        <?php endif; ?>

    </div>

    <?php include 'rightsidebar.php'; ?>

    <script>

        function openRightSidebar(fileId, fileName, filePath, fileType) {
            const sidebar = document.getElementById('rightsidebar');
            if (!sidebar) {
                console.error('Sidebar not found');
                return;
            }

            sidebar.classList.add('open');
            
            document.getElementById('fileId').value = fileId;
            document.getElementById('fileName').value = fileName;
            document.getElementById('fileType').value = fileType;

            const manageTagsButton = document.getElementById('manageTagsButton');
            manageTagsButton.href = `tags_on_file.php?fileId=${fileId}`;  

            updateFilePreview(filePath);
        }

        function updateFilePreview(filePath) {
            const filePreviewContainer = document.getElementById('filePreviewContainer');
            filePreviewContainer.innerHTML = ''; 

            if (filePath.endsWith('.jpg') || filePath.endsWith('.jpeg') || filePath.endsWith('.png') || filePath.endsWith('.gif')) {
                const imgContainer = document.createElement('div');
                imgContainer.classList.add('image-preview');

                const img = document.createElement('img');
                img.src = filePath;
                img.alt = 'File Preview';
                img.classList.add('image');

                imgContainer.appendChild(img);
                filePreviewContainer.appendChild(imgContainer);

            } else if (filePath.endsWith('.txt')) {
                fetch(filePath)
                    .then(response => response.text()) 
                    .then(text => {
                        const textPreview = document.createElement('div');
                        textPreview.classList.add('text-preview');
                        textPreview.textContent = text;
                        filePreviewContainer.appendChild(textPreview);
                    })
                    .catch(err => console.error('Error fetching text file:', err));

            } else if (filePath.endsWith('.pdf')) {
                const pdfEmbed = document.createElement('embed');
                pdfEmbed.src = filePath;
                pdfEmbed.type = 'application/pdf';
                pdfEmbed.classList.add('pdf-preview');
                filePreviewContainer.appendChild(pdfEmbed);

            } else if (filePath.endsWith('.mp4') || filePath.endsWith('.webm') || filePath.endsWith('.ogg')) {
                const video = document.createElement('video');
                video.src = filePath;
                video.controls = true;
                video.classList.add('video-preview');
                filePreviewContainer.appendChild(video);

            } else if (filePath.endsWith('.mp3') || filePath.endsWith('.wav') || filePath.endsWith('.ogg')) {
                const audio = document.createElement('audio');
                audio.src = filePath;
                audio.controls = true;
                audio.classList.add('audio-preview');
                filePreviewContainer.appendChild(audio);

            } else {
                filePreviewContainer.innerHTML = 'Preview not available for this file type.';
            }
        }

        function closeRightSidebar() {
            document.getElementById('rightsidebar').classList.remove('open');
        }
    </script>

</body>
</html>
