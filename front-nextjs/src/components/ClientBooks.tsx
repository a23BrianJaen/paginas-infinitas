
import { Book } from "@/types/types";
import BookCard from "./common/BookCard";

export default function ClientBooks({ books }: { books: Book[] }) {
  // Convert object to array if needed
  const booksArray = Object.values(books);

  return (
    <div className="w-full">
      <h1 className="text-3xl font-bold mb-8 text-center">Our Books Collection</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 p-4">
        {booksArray.map((book) => (
          <BookCard key={book.id} book={book} />
        ))}
      </div>
    </div>
  );
}