import { redirect } from "next/navigation";

export default function Home() {
  /**this page is unused for now -> redirects to dashboard */
  redirect("/dashboard");
  return (
    <main className="min-h-screen flex flex-col items-center">nic take</main>
  );
}
