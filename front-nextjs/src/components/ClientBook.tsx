import { Book } from "@/types/types";
import Image from "next/image";

export default function ClientBooks(book: Book) {

  console.log(book);

  return (
    <div className="flex flex-row place-content-center min-h-screen">
      <div className="flex justify-end items-center">
        <Image
          className="rounded-xl"
          src={book.coverImage}
          alt={book.title}
          width={240}
          height={320}
          style={{
            objectFit: 'cover',
          }}
        />
      </div>

      <div className="flex flex-col place-content-center ml-5">
        <h1 className="text-xl w-fit relative group">
          <span>Titulo: <strong>{book.title}</strong></span>
          <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
        </h1>
        <h1 className="text-xl w-fit relative group">
          <span>Autor: <strong>{book.author}</strong></span>
          <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
        </h1>
        <h1 className="text-xl w-fit relative group">
          <span>Genero: <strong>{book.genre}</strong></span>
          <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
        </h1>
        <h1 className="text-xl w-fit relative group">
          <span>Año de lanzamiento: <strong>{book.year}</strong></span>
          <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
        </h1>
        <h1 className="text-xl w-fit relative group">
          <span>ISBN: <strong>{book.isbn}</strong></span>
          <span className="absolute bottom-0 left-1/2 w-0 h-[1px] bg-amber-100 group-hover:w-full group-hover:left-0 transition-all duration-500 ease-in-out"></span>
        </h1>
      </div>
    </div>
  )
}