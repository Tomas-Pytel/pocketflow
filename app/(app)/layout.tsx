"use client";

import DesktopLayout from "@/components/layouts/desktop-layout";
import MobileLayout from "@/components/layouts/mobile-layout";
import { useMediaQuery } from "@/hooks/useMediaQuery";

export default function AppLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const isDesktop = useMediaQuery("(min-width: 768px)");
  return isDesktop ? (
    <DesktopLayout>{children}</DesktopLayout>
  ) : (
    <MobileLayout>{children}</MobileLayout>
  );
}
