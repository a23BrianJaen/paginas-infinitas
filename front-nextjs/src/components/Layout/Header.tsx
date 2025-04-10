import Navbar from "./Navbar";

export default function Header() {
  return (
    <div className="flex flex-col items-center bg-[#282828] shadow-md shadow-gray-300 p-3">
      <header className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-white drop-shadow-[0_0_0.3rem_#ffffff70]">Paginas infinitas</h1>
      </header>
      <Navbar />
    </div>
  )
}