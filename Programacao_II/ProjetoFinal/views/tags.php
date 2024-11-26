<?php
require "../Models/Tag.php";
require "../Repositories/TagRepository.php";
require "../Repositories/FileTagRepository.php";

$tagRepo = new TagRepository();
$fileTagRepo = new FileTagRepository();

$tags = $tagRepo->getAllTags();

function getFileCountForTag($tagId) {
    global $fileTagRepo;
    $fileAssociations = $fileTagRepo->getFilesOnTag($tagId);
    return $fileAssociations ? count($fileAssociations) : 0;
}

foreach ($tags as $tag) {
    $tag->fileCount = getFileCountForTag($tag->getId());
}

usort($tags, function($a, $b) {
    if ($a->fileCount == $b->fileCount) {
        return strcmp($a->getName(), $b->getName()); // Alphabetical sorting if counts are the same
    }
    return $b->fileCount - $a->fileCount; // Sort by file count in descending order
});
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tags - TagScribe</title>
    <link rel="stylesheet" href="styles.css">
    <style>
        .tags-list {
            margin-top: 20px;
        }

        .tag-item {
            background-color: #f4f4f4;
            padding: 10px;
            margin: 5px 0;
            border-radius: 5px;
            display: flex;
            justify-content: space-between; 
            align-items: center;
            cursor: pointer;
        }

        .tag-item:hover {
            background-color: #e0e0e0;
        }

        .tag-info {
            display: flex;
            align-items: center;
            justify-content: flex-start; 
            gap: 10px; 
            width: auto;
        }

        .tag-name {
            font-weight: bold;
            font-size: 16px;
        }

        .file-count {
            font-size: 14px;
            color: #888;
        }

        .edit-buttons {
            display: flex;
            justify-content: flex-end; 
            gap: 5px; 
        }

        .edit-button, .view-files-button {
            background-color: #4CAF50;
            color: white;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .edit-button:hover, .view-files-button:hover {
            background-color: #45a049;
        }

        .view-files-button {
            background-color: #2196F3;
        }

        .view-files-button:hover {
            background-color: #0b7dda;
        }

    </style>
</head>
<body>

    <?php include 'topbar.php'; ?>

    <div class="content">
        <div class="button-container">
            <form action="addtag.php" method="get">
                <button type="submit" class="styled-button">Add Tag</button>
            </form>
        </div>

        <h1>Tags</h1>

        <div class="tags-list">
            <?php if ($tags): ?>
                <?php foreach ($tags as $tag): ?>
                    <div class="tag-item">
                        <div class="tag-info" onclick="openTagRightSidebar(<?php echo $tag->getId(); ?>, '<?php echo addslashes($tag->getName()); ?>', '<?php echo addslashes($tag->getDescription()); ?>')">
                            <span class="tag-name"><?php echo htmlspecialchars($tag->getName()); ?></span>
                            <span class="file-count">(<?php echo $tag->fileCount; ?> file<?php echo $tag->fileCount > 1 ? 's' : ''; ?>)</span>
                        </div>
                        
                        <div class="edit-buttons">
                            <a href="view_files.php?tagId=<?php echo $tag->getId(); ?>" class="view-files-button">View Files</a>

                            <button onclick="openTagRightSidebar(<?php echo $tag->getId(); ?>, '<?php echo addslashes($tag->getName()); ?>', '<?php echo addslashes($tag->getDescription()); ?>')" class="edit-button">Edit</button>
                        </div>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No tags found.</p>
            <?php endif; ?>
        </div>
    </div>

    <?php include 'tagrightsidebar.php'; ?>

<script>
    function openTagRightSidebar(tagId, tagName, tagDescription) {
        document.getElementById('tagrightsidebar').classList.add('open');  


        document.getElementById('tagId').value = tagId;
        document.getElementById('tagName').textContent = tagName;
        document.getElementById('tagDescription').textContent = tagDescription;
        document.getElementById('tagNameInput').value = tagName;
        document.getElementById('tagDescriptionInput').value = tagDescription;
    }

    function closeTagRightSidebar() {
        document.getElementById('tagrightsidebar').classList.remove('open'); 
    }

    function deleteTag() {
        const tagId = document.getElementById('tagId').value;
            fetch('delete_tag.php', {
                method: 'POST',
                body: JSON.stringify({ tagId: tagId }),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(data => {
                if (data) {
                    closeTagRightSidebar();
                    location.reload(); 
                } else {
                    alert('Failed to delete tag.');
                }
            })
            .catch(err => console.log(err));
    }
</script>

</body>
</html>
