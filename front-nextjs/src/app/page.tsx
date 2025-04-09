'use client'

import Link from "next/link";

export default function Home() {
  return (
    <div className="flex flex-col min-h-screen min-w-screen">
      <div className="flex flex-col items-center bg-[#282828] shadow-md shadow-gray-300 p-2">
        <div className=" ">
          <nav className="flex gap-4">
            <Link href="/">Inicio</Link>
            <Link href="/books">Catálogo de libros</Link>
            <Link href="/">Contact</Link>
          </nav>
        </div>
      </div>
      <main>
        <h2 className="text-center text-2xl">Paginas infinitas</h2>
      </main>
    </div>
  );
}
