import Link from "next/link";

export default function NavigationLink({
  href,
  children,
  className = "",
}: {
  href: string;
  children: React.ReactNode;
  className?: string;
}) {
  return (
    <Link
      href={href}
      className={`text-orange-500 hover:text-orange-700 px-3 py-2 rounded-md text-xs font-medium ${className}`}
    >
      {children}
    </Link>
  );
}
