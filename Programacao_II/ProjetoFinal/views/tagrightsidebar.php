<head>
    <style>
        .tagrightsidebar {
            position: fixed; 
            top: 50px; 
            right: -350px; 
            width: 300px;
            height: calc(100vh - 60px); 
            background-color: #f4f4f4;
            border-left: 2px solid #ddd;
            padding: 20px;
            box-shadow: -2px 0 10px rgba(0, 0, 0, 0.1);
            z-index: 1000;
            transition: right 0.3s ease; 
        }


        .tagrightsidebar.open {
            right: 0; 
        }

        .tagrightsidebar .close-btn {
            background: none;
            border: none;
            font-size: 18px;
            color: #333;
            cursor: pointer;
            position: absolute;
            top: 20px;
            right: 20px;
        }

        .tagrightsidebar h2 {
            margin-top: 0;
        }

        .tagrightsidebar input,
        .tagrightsidebar select,
        .tagrightsidebar textarea {
            width: 100%;
            padding: 8px;
            margin: 10px 0;
            border: 1px solid #ddd;
            border-radius: 4px;
        }

        .tagrightsidebar button {
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 10px;
        }

        .tagrightsidebar button:hover {
            background-color: #45a049;
        }

        .tagrightsidebar .tag-description {
            margin: 10px 0;
            font-size: 16px;
            color: #555;
        }

        .tagrightsidebar .tag-name {
            font-weight: bold;
            font-size: 20px;
        }
    </style>
</head>

<div class="tagrightsidebar" id="tagrightsidebar">
    <button class="close-btn" onclick="closeTagRightSidebar()">×</button>
    <h2>Tag Details</h2>

    <div class="tag-details">
        <p class="tag-name" id="tagName">Tag Name</p>
        <p class="tag-description" id="tagDescription">Tag description will appear here...</p>
    </div>

    <form id="tagForm" method="post" action="update_tag.php">
        <input type="hidden" name="tagId" id="tagId" value="">

        <label for="tagNameInput">Tag Name:</label>
        <input type="text" id="tagNameInput" name="tagName" required>

        <label for="tagDescriptionInput">Tag Description:</label>
        <textarea id="tagDescriptionInput" name="tagDescription" rows="4" required></textarea>

        <button type="submit">Save Changes</button>
        <button type="button" onclick="deleteTag()">Delete Tag</button>
    </form>
</div>

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
        if (confirm('Are you sure you want to delete this tag?')) {
            fetch('delete_tag.php', {
                method: 'POST',
                body: JSON.stringify({ tagId: tagId }),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Tag deleted successfully!');
                    closeTagRightSidebar();
                    location.reload(); 
                } else {
                    alert('Failed to delete tag.');
                }
            })
            .catch(err => console.log(err));
        }
    }
</script>
