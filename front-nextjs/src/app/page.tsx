'use client'

export default function Home() {
  return (
    <div className="flex flex-col min-h-screen bg-gradient-to-b from-gray-900 to-gray-800">
      <div className="relative overflow-hidden bg-black/30 py-4 shadow-lg">
        <div className="animate-scrolling-text whitespace-nowrap text-amber-100/90 text-xl font-semibold">
          📚 Descubre mundos infinitos a través de nuestras páginas • Explora historias que transformarán tu manera de ver el mundo • Tu próxima aventura literaria está a un click de distancia 🌟
        </div>
      </div>

      <main className="flex-1 flex flex-col items-center justify-center p-8 text-center">
        <h1 className="text-5xl font-bold mb-6 text-white drop-shadow-[0_0_0.3rem_#ffffff70]">
          Páginas Infinitas
        </h1>
        <p className="text-xl text-gray-300 max-w-2xl mb-8">
          Sumérgete en nuestra colección cuidadosamente seleccionada de libros que inspiran, entretienen y transforman.
        </p>
        <a
          href="/books"
          className="px-8 py-3 bg-amber-100/10 text-amber-100 rounded-lg border border-amber-100/20 hover:bg-amber-100/20 transition-all duration-300 text-lg"
        >
          Explorar Catálogo
        </a>
      </main>
    </div>
  );
}
