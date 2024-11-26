<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Add Tag - TagScribe</title>
    <link rel="stylesheet" href="styles.css">
    
    <style>
        .add-tag-form {
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .add-tag-form h1 {
            color: #333;
        }

        .add-tag-form form {
            display: flex;
            flex-direction: column;
        }

        .add-tag-form label {
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        .add-tag-form input[type="text"],
        .add-tag-form textarea {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin-bottom: 15px;
            font-size: 16px;
        }

        .add-tag-form textarea {
            resize: vertical;
        }

        .add-tag-form button {
            padding: 10px 15px;
            background-color: #333;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

        .add-tag-form button:hover {
            background-color: #555;
        }

        .error {
            color: red;
            font-weight: bold;
            margin-bottom: 20px;
        }
    </style>
</head>

<body>
    <?php include 'topbar.php'; ?>

    <div class="content add-tag-form">
        <h1>Add Tag</h1>

        <?php if (isset($_GET['error'])): ?>
            <p class="error">
                <?php
                
                if ($_GET['error'] == 'empty_fields') {
                    echo "Both fields are required. Please fill in all fields.";
                } elseif ($_GET['error'] == 'add_failed') {
                    echo "There was an error adding the tag. Please try again.";
                }
                ?>
            </p>
        <?php endif; ?>

        <form action="submit_tag.php" method="post">
            <div>
                <label for="tag-name">Name:</label>
                <input type="text" id="tag-name" name="tag-name" required placeholder="Enter tag name" value="<?php echo isset($_GET['name']) ? htmlspecialchars($_GET['name']) : ''; ?>">
            </div>
            <div>
                <label for="tag-description">Description:</label>
                <textarea id="tag-description" name="tag-description" rows="4" required placeholder="Enter tag description"><?php echo isset($_GET['description']) ? htmlspecialchars($_GET['description']) : ''; ?></textarea>
            </div>
            <button type="submit">Add Tag</button>
        </form>
    </div>
</body>
</html>
