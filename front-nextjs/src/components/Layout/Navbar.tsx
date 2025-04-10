import Link from "next/link";

export default function Navbar() {
  return (

    <nav className="flex gap-4">
      <Link href="/">Inicio</Link>
      <Link href="/books">Catálogo de libros</Link>
      <Link href="/">Contact</Link>
    </nav>
  );
}