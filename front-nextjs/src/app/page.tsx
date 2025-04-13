'use client'

export default function Home() {
  return (
    <div className="flex flex-col min-h-screen bg-gradient-to-b from-gray-900 to-gray-800">
      <div className="relative overflow-hidden bg-black/30 py-4 shadow-lg">
        <div className="animate-scrolling-text whitespace-nowrap text-amber-100/90 text-xl font-semibold">
          📚 Discover infinite worlds through our pages • Explore stories that will transform your way of seeing the world • Your next literary adventure is just a click away 🌟
        </div>
      </div>

      <main className="flex-1 flex flex-col items-center justify-center p-8 text-center">
        <h1 className="text-5xl font-bold mb-6 text-white drop-shadow-[0_0_0.3rem_#ffffff70]">
          Infinite Pages
        </h1>
        <p className="text-xl text-gray-300 max-w-2xl mb-8">
          Immerse yourself in our carefully curated collection of books that inspire, entertain and transform.
        </p>
        <a
          href="/books"
          className="px-8 py-3 bg-amber-100/10 text-amber-100 rounded-lg border border-amber-100/20 hover:bg-amber-100/20 transition-all duration-300 text-lg"
        >
          Explore Catalog
        </a>
      </main>
    </div>
  );
}
