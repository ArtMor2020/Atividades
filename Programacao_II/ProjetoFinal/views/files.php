<?php
require_once '../Repositories/FileRepository.php';
require_once '../Repositories/FileTagRepository.php';
require_once '../Repositories/TagRepository.php';

$fileRepo = new FileRepository();   
$fileTagRepo = new FileTagRepository(); 
$tagRepo = new TagRepository();

$files = $fileRepo->getAllFiles();

$filesByType = [];
foreach ($files as $file) {
    $type = $file->getType(); 
    if (!isset($filesByType[$type])) {
        $filesByType[$type] = [];
    }
    $filesByType[$type][] = $file;
}

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Files - TagScribe</title>
    <link rel="stylesheet" href="styles.css">
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
        }

        .content {
            margin-top: 50px; 
            margin-left: 220px; 
            margin-right: 320px; 
            padding: 20px; 
            overflow-y: auto; 
            height: calc(100vh - 60px); 
        }

        .main-container {
            display: flex;
            flex-direction: row;
            min-height: 100vh;
        }

        .sidebar-left {
            position: fixed;
            left: 0;
            top: 0;
            width: 200px;
            background-color: #f4f4f4;
            height: 100vh;
            padding: 20px;
            border-right: 1px solid #ddd;
            z-index: 10;
        }

        .sidebar-right {
            position: fixed;
            right: 0;
            top: 0;
            width: 300px;
            background-color: #f4f4f4;
            height: 100vh;
            padding: 20px;
            border-left: 1px solid #ddd;
            display: none;
            z-index: 100;
            box-shadow: -2px 0 10px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease;
        }

        .sidebar-right.open {
            display: block;
            transform: translateX(0);
        }

        .file-type-section {
            margin-bottom: 30px;
        }

        .file-gallery {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
        }

        .file-item {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 10px;
            width: 150px;
            text-align: center;
            cursor: pointer;
        }

        .file-thumbnail img {
            width: 100%;
            max-height: 150px;
            object-fit: cover;
            border-radius: 4px;
        }

        .file-details {
            margin-top: 10px;
        }

        .tags {
            margin-top: 10px;
        }

        .tag {
            display: inline-block;
            background-color: #ddd;
            padding: 5px;
            margin: 2px;
            border-radius: 4px;
            font-size: 12px;
        }
    </style>
</head>
<body>

<?php include 'topbar.php'; ?>

<div class="main-container">
    <div class="content">
        <div class="button-container">
            <form action="addfile.php" method="get">
                <button type="submit" class="styled-button">Add File</button>
            </form>
        </div>

        <?php foreach ($filesByType as $type => $files): ?>
            <div class="file-type-section">
                <h2><?php echo ucfirst($type); ?> Files</h2>
                <div class="file-gallery">
                    <?php foreach ($files as $file): ?>
                        <?php $tags = $fileTagRepo->getTagsOnFile($file->getId()); ?>
                        <div class="file-item" onclick="openRightSidebar(<?php echo $file->getId(); ?>, '<?php echo htmlspecialchars($file->getName()); ?>', '<?php echo htmlspecialchars($file->getPath()); ?>', '<?php echo htmlspecialchars($file->getType()); ?>')">
                            <div class="file-thumbnail">
                                <?php
                                if ($type === 'image' && file_exists($file->getPath())):
                                    echo "<img src='" . htmlspecialchars($file->getPath()) . "' alt='" . htmlspecialchars($file->getName()) . "' class='file-image'>";
                                else:
                                    echo "<img src='default-icon.png' alt='File' class='file-icon'>";
                                endif;
                                ?>
                            </div>
                            <div class="file-details">
                                <h3><?php echo htmlspecialchars($file->getName()); ?></h3>
                                <?php if ($tags): ?>
                                    <div class="tags">
                                        <?php foreach ($tags as $tag): ?>
                                            <?php $name = $tagRepo->getTag($tag['tagId'])->getName()?>
                                            <span class="tag"><?php echo $name; ?></span>
                                        <?php endforeach; ?>
                                    </div>
                                <?php endif; ?>
                            </div>
                        </div>
                    <?php endforeach; ?>
                </div>
            </div>
        <?php endforeach; ?>
    </div>

    <?php include 'rightsidebar.php'; ?>
</div>

<script>

    function deleteFile() {
        const fileId = document.getElementById('fileId').value;

        fetch('delete_file.php', {
            method: 'POST',
            body: JSON.stringify({ fileId: fileId }),
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data === true) { 
                closeRightSidebar(); 
                location.reload(); 
            } else { 
                alert('Failed to delete file.');
            }
        })
        .catch(err => {
            console.log('Error:', err);
            alert('An error occurred while deleting the file.');
        });
}

    function handleDeleteResponse(response) {
        if (response.status === 'success') {
            showNotification('success', response.message);
        } else {
            showNotification('error', response.message);
        }
    }
</script>

</body>
</html>