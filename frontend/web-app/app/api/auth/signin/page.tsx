import EmptyFilter from '@/app/components/EmptyFilter'
import React from 'react'

type Props = {
  searchParams: Promise<{
    callbackUrl?: string;
  }>;
};

export default async function SignIn({ searchParams }: Props) {
    const { callbackUrl } = await searchParams;
  return (
    <EmptyFilter 
        title='You need to be logged in to do that'
        subtitle='Please login below'
        showLogin 
        callbackUrl={callbackUrl}
    />
  )
}
