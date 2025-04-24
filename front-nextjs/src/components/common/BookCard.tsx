'use client'

import { Book } from "@/types/types";
import Image from "next/image";
import { useRouter, usePathname } from "next/navigation"

export default function BookCard({ book }: { book: Book }) {

  const router = useRouter();
  const pathname = usePathname();

  const handleImageClick = (id: number) => {
    router.push(`${pathname}/${id}`);
  }

  return (
    <div className="drop-shadow-[0_0_0.6rem_#ffffff90] hover:drop-shadow-[0_0_0.6rem_#fef3c6] scale-100 hover:scale-[1.03] transition duration-200 rounded-xl">
      <Image
        className="rounded-xl cursor-pointer"
        src={book.coverImage}
        alt={book.title}
        width={240}  // w-60 = 240px
        height={320} // h-80 = 320px
        style={{
          objectFit: 'cover',
        }}
        onClick={() => {
          handleImageClick(book.id)
        }}
      />
    </div>
  );
}