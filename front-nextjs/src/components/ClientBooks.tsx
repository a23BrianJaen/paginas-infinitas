
import { Book } from "@/types/types";
import BookCard from "./common/BookCard";

export default function ClientBooks({ books }: { books: Book[] }) {
  // Convert object to array if needed
  const booksArray = Object.values(books);

  console.log(booksArray);

  return (
    <div className="w-full">
      <h1 className="text-3xl font-bold mb-8 mt-4 text-center drop-shadow-[0_0_0.3rem_#ffffff70]">
        Toda nuestra colección de libros
      </h1>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 p-4 place-items-center">
        {booksArray.map((book) => (
          <BookCard key={book.id} book={book} />
        ))}
      </div>
    </div>
  );
}