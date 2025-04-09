
import { Book } from "@/types/types";

export default function ClientBooks({ books }: { books: Book[] }) {
  // Convert object to array if needed
  const booksArray = Object.values(books);

  return (
    <div className="w-full">
      <h1 className="text-3xl font-bold mb-8 text-center">Our Books Collection</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 p-4">
        {booksArray.map((book) => (
          <div key={book.id} className="bg-white rounded-lg shadow-md p-4">
            <h2 className="text-xl font-semibold mb-2">{book.title}</h2>
            <p className="text-gray-400">Author: {book.author}</p>
          </div>
        ))}
      </div>
    </div>
  );
}