import Link from "next/link";
import { House, Book, BookOpen, ChevronRight } from "lucide-react"

interface breadCumb {
  title: string;
}

export default function BreadCumb(title: breadCumb) {
  const bookTitle = title.title;

  return (
    <nav className="flex flex-row ml-5 mt-5 w-fit px-4 py-3  from-gray-900 to-gray-800 text-gray-700 border border-[#e7e7e7] rounded-lg dark:border-[#e7e7e7]" aria-label="Breadcrumb">
      <ol className="inline-flex items-center space-x-1 md:space-x-2 rtl:space-x-reverse">
        <li className="inline-flex items-center">
          <Link href="/" className="inline-flex items-center text-gray-400 hover:text-amber-100 transition-colors duration-300 text-sm font-medium hover:drop-shadow-[0_0_0.6rem_#ffffff70]">
            <House className="w-4 h-4 font-bold mr-2" />
            Home
          </Link>
        </li>
        <li>
          <div className="flex items-center">
            <ChevronRight className="w-4 h-4 text-[#e7e7e7]" strokeWidth={3} />
            <Link href="/books" className="inline-flex items-center text-gray-400 hover:text-amber-100 transition-colors duration-300 text-sm font-medium hover:drop-shadow-[0_0_0.6rem_#ffffff70]">
              <Book className="w-4 h-4 font-bold mr-2" />
              Book collection
            </Link>
          </div>
        </li>
        <li aria-current="page">
          <div className="flex items-center">
            <ChevronRight className="w-4 h-4 text-[#e7e7e7]" strokeWidth={3} />
            <span className="inline-flex items-center text-gray-400 hover:text-amber-100 transition-colors duration-300 text-sm font-medium hover:drop-shadow-[0_0_0.6rem_#ffffff70]">
              <BookOpen className="w-4 h-4 font-bold mr-2" />
              {bookTitle}
            </span>
          </div>
        </li>
      </ol>
    </nav>

  );
}