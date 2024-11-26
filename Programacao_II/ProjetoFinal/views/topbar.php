<head>
    <style>
        .top-bar {
            background-color: #333;
            color: white;
            padding: 10px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            position: fixed; 
            top: 0;
            left: 0;
            right: 0;
            z-index: 1001; 
            height: 30px; 
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); 
        }

        .top-bar {
            background-color: #333;
            color: white;
            padding: 10px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .top-bar .logo a {
            color: white;
            text-decoration: none;
            font-size: 24px;
        }

        nav ul {
            list-style: none;
            padding: 0;
            margin: 0;
            display: flex;
        }

        nav ul li {
            margin-left: 20px;
        }

        nav ul li a {
            color: white;
            text-decoration: none;
        }

        nav ul li a:hover {
            text-decoration: underline;
        }

        .search-form {
            display: flex;
            align-items: center;
            margin-left: 20px; 
        }

        .search-form input[type="text"] {
            padding: 5px;
            border: none;
            border-radius: 3px;
        }

        .search-form button {
            padding: 5px 10px;
            background-color: #555;
            color: white;
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }

        .search-form button:hover {
            background-color: #777;
        }

        .leftsidebar-button {
            padding: 10px 16px; 
            background-color: #444;
            color: white;
            border: 1px solid #666;
            border-radius: 8px; 
            font-size: 18px; 
            cursor: pointer;
            text-align: center;
            transition: background-color 0.3s ease, transform 0.3s ease;
            flex: 1; 
            margin: 0 10px;
            top: 30;
        }

        .leftsidebar-button:hover {
            background-color: #666;
            transform: translateY(-2px);
        }

        .leftsidebar-button:active {
            background-color: #333;
            transform: translateY(1px); 
        }

        .leftsidebar-button:focus {
            outline: none;
            box-shadow: 0 0 3px 2px rgba(255, 255, 255, 0.6);
        }

        .leftsidebar {
            width: 200px; 
            background-color: #f4f4f4;
            padding: 20px;
            position: fixed; 
            left: 0;
            top: 50px; 
            bottom: 0; 
            height: calc(100vh - 60px); 
            overflow-y: auto; 
            z-index: 999;
        }

        .button-container-leftsidebar {
            display: flex; 
            justify-content: center; 
            align-items: center; 
            gap: 10px;
            flex-direction: row;
        }
    </style>
</head>

<div class="top-bar">
    <div class="logo">
        <a href="index.php">TagScribe</a>
    </div>
    <form class="search-form" action="search.php" method="GET">
        <input type="text" name="query" placeholder="Search Tags and Files" required>
        <button type="submit">Search</button>
    </form>

    <div class="button-container-leftsidebar">
        <form action="tags.php" method="get">
            <button type="submit" class="leftsidebar-button">TAGS</button>
        </form>
        <form action="files.php" method="get">
            <button type="submit" class="leftsidebar-button">FILES</button>
        </form>
    </div>

    <nav>
        <ul>
            <li><a href="backup.php">Backup</a></li>
        </ul>
    </nav>
</div>