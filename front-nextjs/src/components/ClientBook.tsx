import { Book } from "@/types/types";
import Image from "next/image";
import BreadCumb from "./Layout/BreadCumb";

export default function ClientBooks(book: Book) {
  return (
    <div>
      <BreadCumb title={book.title} />
      <div className="flex flex-row place-content-center items-center min-h-screen gap-16 p-8">
        <div className="flex justify-end items-center">
          <div className="relative group">
            <Image
              className="rounded-2xl hover:drop-shadow-[0_0_1.5rem_#ffffffa0] transition-all duration-300 ease-in-out cursor-pointer scale-100 hover:scale-105"
              src={book.coverImage}
              alt={book.title}
              width={300}
              height={400}
              style={{
                objectFit: 'cover',
              }}
              priority
            />
            <div className="absolute inset-0 rounded-2xl bg-black opacity-0 group-hover:opacity-10 transition-opacity duration-300"></div>
          </div>
        </div>

        <div className="flex flex-col gap-6 max-w-xl">
          <h1 className="text-3xl font-bold mb-4">{book.title}</h1>

          <div className="space-y-4">
            <h2 className="text-xl w-fit relative group">
              <span>Author: <strong className="text-amber-100">{book.author}</strong></span>
              <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
            </h2>

            <h2 className="text-xl w-fit relative group">
              <span>Genre: <strong className="text-amber-100">{book.genre}</strong></span>
              <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
            </h2>

            <h2 className="text-xl w-fit relative group">
              <span>Year: <strong className="text-amber-100">{book.year}</strong></span>
              <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
            </h2>

            <h2 className="text-xl w-fit relative group">
              <span>ISBN: <strong className="text-amber-100">{book.isbn}</strong></span>
              <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
            </h2>
          </div>
        </div>
      </div>
    </div>
  );
}