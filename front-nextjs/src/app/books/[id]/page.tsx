import ClientBook from "@/components/ClientBook";
import { getBookById } from "@/services/communicationManager";

export default async function Book({ params }: { params: Promise<{ id: number }> }) {

  const { id } = await params;

  const book = await getBookById(id);

  return (
    <ClientBook {...book} />
  )
}