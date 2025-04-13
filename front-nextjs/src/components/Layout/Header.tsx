import Navbar from "./Navbar";
import Link from "next/link";

export default function Header() {
  return (
    <div className="flex flex-row items-center shadow-md p-3">
      <header className="flex justify-between items-center">
        <Link href="/" className="left-2 text-2xl font-bold w-fit text-white drop-shadow-[0_0_0.3rem_#ffffff70] my-2 hover:drop-shadow-[0_0_0.8rem_#ffffff70]">
          I&P
        </Link>
      </header>
      <Navbar />
    </div>
  )
}