"use client";
import Link from "next/link";
import { usePathname } from "next/navigation";

interface NavigationLinkProps {
  href: string;
  title: string;
  children: React.ReactNode;
  className?: string;
}

export default function NavigationLink({
  href,
  title,
  children,
  className = "",
}: NavigationLinkProps) {
  const pathname = usePathname();
  const isActive = pathname === href;

  return (
    <Link
      href={href}
      className={`hover:text-orange-500 px-3 py-2 rounded-md text-xs font-medium ${className} 
      ${isActive ? "text-orange-500" : "text-gray-400"}`}
    >
      {children}
      <span>{title}</span>
    </Link>
  );
}
