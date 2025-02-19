'use client'
import { FormEvent, useState } from "react";
import { z } from "zod";

export default function CreateCodeForm(){
  const [name, setName] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [repassword, setRepassword] = useState<string>('');
  const [errors, setErrors] = useState<{[key:string]:string}>({});

  const formSchema = z.object({
    name: z.string().min(1, { message: 'O nome da empresa é obrigatório.' }),
    password: z.string().min(6, { message: 'A senha deve ter pelo menos 6 caracteres.' }),
    repassword: z.string(),
  }).refine((data) => data.password === data.repassword, {
    message: 'As senhas estão diferente.',
    path: ['repassword'],
  });

  function dataForm(event:FormEvent){
    event.preventDefault();
    const result = formSchema.safeParse({ name, password, repassword });
    console.log(result.error)

    if (!result.success) {
      const formattedErrors: { [key: string]: string } = {};
      result.error.issues.forEach((issue) => {
        formattedErrors[issue.path[0]] = issue.message;
      });
      setErrors(formattedErrors);
      return;
    }
    setErrors({});
  }

  return(
    <form  onSubmit={dataForm} className="space-y-4 border rounded-md p-4 sm:p-6 lg:p-8">
      <div className="flex flex-col gap-2">
        <label htmlFor="name" className="font-semibold">Nome da Empresa</label>

        <div className="relative">
          <input
            type="text"
            className="w-full rounded-lg border border-gray-200 p-2 pe-12 text-base shadow-xs"
            name="name"
            placeholder=""
            onChange={(e)=> setName(e.target.value)}
          />
          {errors.name && <p className="text-red-500 text-sm mt-1">{errors.name}</p>}
        </div>
        
      </div>
  
      <div className="flex flex-col gap-2">
        <label htmlFor="password" className="font-semibold">Senha</label>

        <div className="relative">
          <input
            type="password"
            className="w-full rounded-lg border border-gray-200 p-2 pe-12 text-base shadow-xs"
            name="password"
            placeholder=""
            onChange={(e)=> setPassword(e.target.value)}
          />
        </div>
        {errors.password && <p className="text-red-500 text-sm mt-1">{errors.password}</p>}
      </div>

      <div className="flex flex-col gap-2">
        <label htmlFor="repeatpassoword" className="font-semibold">Repetir Senha</label>

        <div className="relative">
          <input
            type="password"
            className="w-full rounded-lg border border-gray-200 p-2  pe-12 text-base shadow-xs"
            name="repeatpassoword"
            placeholder=""
            onChange={(e)=> setRepassword(e.target.value)}
          />
        </div>
        {errors.repassword && <p className="text-red-500 text-sm mt-1">{errors.repassword}</p>}
      </div>

      <button
        type="submit"
        className="block w-full rounded-lg bg-gray-950 hover:bg-gray-900 px-5 py-3 text-sm font-medium text-white"
      >
        Gerar Código
      </button>
    </form>

  )
}