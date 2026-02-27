import { cn } from "@/lib/utils";
import { Card, CardContent, CardDescription, CardTitle } from "./card";

interface StatCardProps {
  title: string;
  value: string | number;
  className?: string;
}

export default function StatCard({ title, value, className }: StatCardProps) {
  return (
    <Card
      className={cn(
        "flex-1 bg-gray-50 border border-muted-200 dark:bg-gray-800 dark:border dark:border-gray-700 shadow-none",
        className,
      )}
    >
      <CardContent className="p-5 flex justify-start">
        <div className="flex flex-col items-start gap-1.5">
          <CardDescription className="dark:text-white">{title}</CardDescription>
          <CardTitle className="dark:text-gray-900">{value}</CardTitle>
        </div>
      </CardContent>
    </Card>
  );
}
