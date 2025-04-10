'use client'

import { Book } from "@/types/types";
import Image from "next/image";
import { useRouter } from "next/navigation"
import { usePathname } from 'next/navigation'

export default function BookCard({ book }: { book: Book }) {

  const router = useRouter();
  const pathname = usePathname();

  const handleImageClick = (id: number) => {
    router.push(`${pathname}/${id}`);
    console.log(`${pathname}/${id}`);

  }

  return (
    <div className="drop-shadow-[0_0_0.8rem_#ffffff90] rounded-xl">
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