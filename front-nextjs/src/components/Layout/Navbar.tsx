import Link from "next/link";

export default function Navbar() {
  return (
    <nav className="flex gap-6 place-content-center w-full items-center">
      <Link
        href="/"
        className="text-gray-300 hover:text-amber-100 transition-colors duration-300 text-sm font-medium hover:drop-shadow-[0_0_0.6rem_#ffffff70]"
      >
        Inicio
      </Link>
      <Link
        href="/books"
        className="text-gray-300 hover:text-amber-100 transition-colors duration-300 text-sm font-medium hover:drop-shadow-[0_0_0.6rem_#ffffff70]"
      >
        Catálogo de libros
      </Link>
      <Link
        href="/add-book"
        className="text-gray-300 hover:text-amber-100 transition-colors duration-300 text-sm font-medium hover:drop-shadow-[0_0_0.6rem_#ffffff70]"
      >
        Añadir libro
      </Link>
    </nav>
  );
}