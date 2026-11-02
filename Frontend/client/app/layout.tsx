import type { Metadata } from "next";
import "./globals.css";
import PageHeader from "@/Components/PageHeader";
import PageFooter from "@/Components/PageFooter";


export const metadata: Metadata = {
  title: "Norrof - EP2 Healthcare app ",
  description: "A app to look after your health!!",
};

export default function RootLayout({children,}: Readonly<{children: React.ReactNode;}>) {
  return (
    <html lang="en">
      <body className="min-h-screen flex flex-col bg-sky-300 text-[#e2e8f0]">
        <PageHeader />

        <main className="flex-1">
          {children}
        </main>

        <PageFooter />
      </body>
    </html>
  );
}
