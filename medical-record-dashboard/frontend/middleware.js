import { NextResponse } from 'next/server';

export function middleware(request) {
  const isAuthPage = request.nextUrl.pathname === '/';
  const isDashboard = request.nextUrl.pathname.startsWith('/dashboard');
  
  if (isDashboard) {
    return NextResponse.next();
  }
  
  return NextResponse.next();
}

export const config = {
  matcher: ['/dashboard/:path*']
};