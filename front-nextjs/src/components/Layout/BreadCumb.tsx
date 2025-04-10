import Link from "next/link";
import { House, Book, BookOpen, ChevronRight } from "lucide-react"

interface breadCumb {
  title: string;
}

export default function BreadCumb(title: breadCumb) {
  const bookTitle = title.title;

  return (
    <nav className="flex flex-row ml-5 mt-5 w-fit px-4 py-2 text-gray-700 border border-[#e7e7e7] rounded-lg bg-gray-50 dark:bg-[#1c1c1c] dark:border-[#e7e7e7]" aria-label="Breadcrumb">
      <ol className="inline-flex items-center space-x-1 md:space-x-2 rtl:space-x-reverse">
        <li className="inline-flex items-center">
          <Link href="/" className="inline-flex items-center text-sm font-medium text-gray-700 hover:text-blue-600 dark:text-gray-400 dark:hover:text-white">
            <House className="w-4 h-4 font-bold mr-2" />
            Inicio
          </Link>
        </li>
        <li>
          <div className="flex items-center">
            <ChevronRight className="w-4 h-4 text-[#e7e7e7]" strokeWidth={3} />
            <Link href="/books" className="inline-flex items-center text-sm font-medium text-gray-700 hover:text-blue-600 dark:text-gray-400 dark:hover:text-white ml-2">
              <Book className="w-4 h-4 font-bold mr-2" />
              Catálogo de libros
            </Link>
          </div>
        </li>
        <li aria-current="page">
          <div className="flex items-center">
            <ChevronRight className="w-4 h-4 text-[#e7e7e7]" strokeWidth={3} />
            <span className="inline-flex items-center text-sm font-medium text-gray-700 hover:text-blue-600 dark:text-gray-400 dark:hover:text-white ml-2">
              <BookOpen className="w-4 h-4 font-bold mr-2" />
              {bookTitle}
            </span>
          </div>
        </li>
      </ol>
    </nav>

  );
}