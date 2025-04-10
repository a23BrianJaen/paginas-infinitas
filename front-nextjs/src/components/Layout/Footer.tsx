
export default function Footer() {
  return (
    <footer className="flex flex-col items-center justify-center p-8 bg-gray-900 text-gray-300">
      <p className="text-sm">
        &copy; {new Date().getFullYear()} Páginas Infinitas. Todos los derechos reservados.
      </p>
    </footer>
  );
}