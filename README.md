# Infinite Pages - Book Management System

A web application for managing and exploring books. This project consists of a .NET backend API and a Next.js frontend application.

## Project Overview

Infinite Pages allows users to:

- Browse through a collection of books
- View detailed information about each book
- Add new books to the collection
- Manage book information

## Tech Stack

### Backend

- .NET 8.0
- Entity Framework Core
- MySQL Database
- RESTful API

### Frontend

- Next.js
- TypeScript
- Tailwind CSS

## Project Structure

paginas-infinitas/
├── back/
│ └── apiNET/ # .NET Backend API
└── front-nextjs/ # Next.js Frontend Application

## Features

- **Book Browsing**: Navigate through the book collection
- **Book Details**: View comprehensive information about each book
- **Book Management**: Add and update book information
- **Responsive Design**: Fully responsive web interface
- **RESTful API**: Well-structured API endpoints for book operations

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Node.js
- MySQL

### Backend Setup

1. Navigate to the backend directory:

```bash
cd back/apiNET
```

2. Install dependencies:

```bash
dotnet restore
```

3. Set up your database connection in .env

4. Run the application:

```bash
dotnet run
```

### Frontend Setup

1. Navigate to the frontend directory:

```bash
cd front-nextjs
```

2. Install dependencies:

```bash
npm install
```

3. Run the development server:

```bash
npm run dev
```

## API Endpoints

### Books

- `GET /api/books` - Get all books
- `POST /api/books` - Add a new book
- `GET /api/books/{id}` - Get a specific book by ID
- `GET /api/books/search/{title}` - Search books by title
- `GET /api/books/author/{author}` - Search books by author
- `GET /api/books/genre/{genre}` - Search books by genre

Each endpoint returns:
- Success: 200 OK with book(s) data
- Not Found: 404 with appropriate message
- Created: 201 with new book data (POST only)
- **PUT /api/books/{id}**: Update book information
- **DELETE /api/books/{id}**: Delete a book
