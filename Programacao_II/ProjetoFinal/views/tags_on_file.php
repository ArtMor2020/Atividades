<?php
require "../Models/Tag.php";
require "../Repositories/TagRepository.php";
require "../Repositories/FileTagRepository.php";

$tagRepo = new TagRepository();
$fileTagRepo = new FileTagRepository();

$fileId = isset($_GET['fileId']) ? (int) $_GET['fileId'] : 0;

$tagsOnFile = $fileTagRepo->getTagsOnFile($fileId);

if (!$tagsOnFile) {
    $tagsOnFile = [];
}

$fileId = $_GET['fileId'] ?? null;
$tagsOnFile = (new FileTagRepository())->getTagsOnFile($fileId);
$availableTags = (new FileTagRepository())->getAvailableTagsOnFile($fileId);
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tags on File - TagScribe</title>
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

        .tags-list, .other-tags-list {
            margin-top: 20px;
        }

        .tag-item, .other-tag-item {
            background-color: #f4f4f4;
            padding: 10px;
            margin: 5px 0;
            border-radius: 5px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            cursor: pointer;
        }

        .tag-item:hover, .other-tag-item:hover {
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
            background-color: #c81306;
            color: white;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .edit-button:hover, .view-files-button:hover {
            background-color: #710800;
        }

        .view-files-button {
            background-color: #2196F3;
        }

        .view-files-button:hover {
            background-color: #0b7dda;
        }

        .remove-button {
            background-color: #f44336; 
            color: white;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .remove-button:hover {
            background-color: #e53935; 
        }

        .add-button {
            background-color: #4CAF50;
            color: white;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .add-button:hover {
            background-color: #217a24;
        }

    </style>
            
</head>
<body>

    <?php include 'topbar.php'; ?>

    <div class="content">
        <h1>Tags on File</h1>

        <div class="tags-list">
            <?php if ($tagsOnFile): ?>
                <?php foreach ($tagsOnFile as $tag): ?>
                    <div class="tag-item">
                        <div class="tag-info">
                            <span class="tag-name"><?php echo htmlspecialchars($tag['name']); ?></span>
                        </div>
                        <div class="edit-buttons">
                            <button onclick="removeTagAssociation(<?php echo $fileId; ?>, <?php echo $tag['tagId']; ?>)" class="edit-button">Remove</button>
                        </div>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No tags associated with this file.</p>
            <?php endif; ?>
        </div>

        <h2>Other Tags</h2>
        <div class="other-tags-list">
            <?php if ($availableTags): ?>
                <?php foreach ($availableTags as $tag): ?>
                    <div class="other-tag-item">
                        <div class="tag-info">
                            <span class="tag-name"><?php echo htmlspecialchars($tag['name']); ?></span>
                        </div>
                        <div class="other-edit-buttons">
                        <form action="add_tag_to_file.php" method="POST">
                            <input type="hidden" name="fileId" value="<?php echo $fileId; ?>">
                            <input type="hidden" name="tagId" value="<?php echo $tag['tagId']; ?>">
                            <button type="submit" class="add-button">Add</button>
                        </form>
                        </div>
                    </div>
                <?php endforeach; ?>
            <?php else: ?>
                <p>No other tags available to add.</p>
            <?php endif; ?>
        </div>


    </div>

    <?php include 'tagrightsidebar.php'; ?>

</body>

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

    function removeTagAssociation(fileId, tagId) {
            fetch('remove_tag_from_file.php', {
                method: 'POST',
                body: JSON.stringify({ fileId: fileId, tagId: tagId }), 
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json()) 
            .then(data => {
                if (data.success) {
                    location.reload(); 
                } else {
                    alert('Failed to remove tag association: ' + data.error); 
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('An error occurred while removing the tag association.');
            });
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
