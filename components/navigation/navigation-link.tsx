"use client";

import { cn } from "@/lib/utils";
import Link from "next/link";
import { usePathname } from "next/navigation";

interface NavigationLinkProps {
  href: string;
  label: string;
  icon: React.ElementType;
  variant: "sidebar" | "mobile";
}

export default function NavigationLink({
  href,
  label,
  icon: Icon,
  variant,
}: NavigationLinkProps) {
  const pathname = usePathname();
  const isActive = pathname === href;

  const baseStyles =
    "flex items-center justify-center font-medium transition-all duration-200";

  const sidebarStyles = cn(
    "flex-row justify-start gap-3 px-4 py-2 rounded-md w-full text-md",
    isActive
      ? "bg-gradient-to-r from-orange-500 to-yellow-500 text-white"
      : "text-gray-400 hover:text-orange-400",
  );

  const mobileStyles = cn(
    "flex-col gap-1 flex-1 py-2 text-xs",
    isActive ? "text-orange-500" : "text-slate-400",
  );

  return (
    <Link
      href={href}
      className={cn(
        baseStyles,
        variant === "sidebar" ? sidebarStyles : mobileStyles,
      )}
    >
      <Icon
        className={cn(
          variant === "sidebar" ? "h-5 w-5" : "h-6 w-6",
          isActive ? "" : "opacity-70",
        )}
      />
      <span>{label}</span>
    </Link>
  );
}
