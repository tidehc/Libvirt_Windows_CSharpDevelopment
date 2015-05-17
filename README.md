<h3>Preamble:</h3>
<p>This library is designed to be a 1 to 1 mapping from c# to the libvirt C API maintaining naming convention and adding no additional helper functions.</p>
<h3>Nuget Package Usage:</h3>
<ul>
<li>
  Add Nuget package to your project.
  <ul>
  <li>
  This can be done by using the package manager console and typeing 'Install-Package Libvirt_Pinvoke' 
  </li>
  <li>
    -- or --
  </li>
  <li>
    via the GUI by searching for Libvirt_PInvoke
  </li>
  </ul>
</li>
  <li>
 Project must be either x86 or x64 build to ensure correct dll's are linked properly. ANY CPU is not a valid build configuration and a compiler error will be generated if you try to build with 'ANY CPU.'
  </li>
</ul>


<h3>API:</h3>
<ul>
 <li>
 The reference API is here http://libvirt.org/html/. All functions and structs use the same names as are used in the API.
 </li>
  <li>
 All Structures, delegates, enums -- everything except function calls are in the Libvirt namespace.
 <ul>
 <li>
 For example, in the libvirt-host api listing, there is a a type struct virSecurityModel, it can be referenced within your c# code as Libvirt.virSecurityModel
 </li>
 </ul>
 </li>
   <li>
 All Function calls are inside the Libvirt.PInvoke namespace
  <li>
Ror example the libvirt-host api listing has a function named virTypedParamsAddDouble. This function can be called in c# via Libvirt.PInvoke.virTypedParamsAddDouble(....)
 </li>
 </li>
</ul>
<br/> 


