"use client";

import { NAV_LINKS } from "@/constants/navigation";
import NavigationLink from "./navigation-link";

export default function BottomNavigation() {
  return (
    <nav className=" md:hidden fixed bottom-0 left-0 right-0 h-20 px-7 bg-white border-t border-gray-200 dark:bg-gray-800 dark:border-gray-700 z-50">
      <ul className="flex items-center justify-between h-full">
        {NAV_LINKS.map((link, index) => (
          <li key={index}>
            <NavigationLink
              href={link.href}
              label={link.label}
              icon={link.icon}
              variant="mobile"
            />
          </li>
        ))}
      </ul>
    </nav>
  );
}
