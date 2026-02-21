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
      className={` px-3 py-2 rounded-md font-medium ${className} 
      ${
        isActive
          ? " dark:bg-orange-500/20 bg-gradient-to-r from-orange-500 to-yellow-500 text-white"
          : "text-gray-400 hover:text-orange-400 dark:hover:text-orange-400"
      }`}
    >
      {children}
      <span>{title}</span>
    </Link>
  );
}
