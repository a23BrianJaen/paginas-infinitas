import { Book } from "@/types/types";

export default function BookCard({ book }: { book: Book }) {
  return (
    <div className="">
      <h2 className="">{book.title}</h2>
      <p className="">Author: {book.author}</p>
    </div>
  );
}